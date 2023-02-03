﻿using MainEntityProject.Model.Context;
using MainEntityProject.Controls.Common;
using MainEntityProject.Generation;
using MainEntityProject.Generation.TankFactories;
using MainEntityProject.LocalService;

namespace MainEntityProject.Controls.Applications
{
    public class TankFactoryApplication : IApplication
    {

        private IServiceCollection serviceCollection;


        public async Task Start()
        {

            List<ITankFactory> list = new()
            {
                new FrenchTankFactory(),
                new GermanTankFactory(),
                new SwedishTankFactory(),
                new JapaneseTankFactory()
            };

            DatabaseDialer visitor = new(this);

            foreach (var item in list)
            {
                await visitor.AddTank(item.CreateNativeTank());
                await visitor.AddTank(item.CreateImportedTank());
            }
        }


        private async Task AddTank(MainBattleTank vehicle)
        {
            var context = GetProvider().GetRequiredService<VehicleDatabaseContext>();

            var duplicate = await context.Tanks.FirstOrDefaultAsync(t => t.ModelName.Equals(vehicle.ModelName));

            if (duplicate is null)
            {
                duplicate = vehicle.Clone() as MainBattleTank;

                if (vehicle.GunReference.PriceReference is not null)
                {
                    Price gunPrice = context.Prices.Where(p => p.Value.Equals(vehicle.GunReference.PriceReference.Value) &&
                    p.Currency.Equals(vehicle.GunReference.PriceReference.Currency)).ToList().FirstOrDefault();

                    if (gunPrice is null)
                        await context.Prices.AddAsync(vehicle.GunReference.PriceReference);
                    else
                    {
                        duplicate.GunReference.PriceReference = gunPrice;
                        duplicate.GunReference.PriceId = gunPrice.Id;
                    }
                }


                if (vehicle.EngineReference.PriceReference is not null)
                {
                    Price enginePrice = context.Prices.Where(p => p.Value.Equals(vehicle.EngineReference.PriceReference.Value) &&
                    p.Currency.Equals(vehicle.EngineReference.PriceReference.Currency)).ToList().FirstOrDefault();

                    if (enginePrice is null)
                        await context.Prices.AddAsync(vehicle.EngineReference.PriceReference);
                    else
                    {
                        duplicate.EngineReference.PriceReference = enginePrice;
                        duplicate.EngineReference.PriceId = enginePrice.Id;
                    }
                }


                if (vehicle.PriceReference is not null)
                {
                    Price tankPrice = context.Prices.Where(p => p.Value.Equals(vehicle.PriceReference.Value) &&
                    p.Currency.Equals(vehicle.PriceReference.Currency)).ToList().FirstOrDefault();

                    if (tankPrice is null)
                        await context.Prices.AddAsync(vehicle.PriceReference);
                    else
                    {
                        duplicate.PriceReference = tankPrice;
                        duplicate.PriceId = tankPrice.Id;
                    }
                }



                if (vehicle.GunReference.ManufacturerReference.BudgetReference is not null)
                {
                    Budget gunManufacturerBudget = context.Budgets.Where(b => b.Value.Equals(vehicle.GunReference.ManufacturerReference.BudgetReference.Value) &&
                    b.Currency.Equals(vehicle.GunReference.ManufacturerReference.BudgetReference.Currency)).ToList().FirstOrDefault();

                    if (gunManufacturerBudget is null)
                        await context.Budgets.AddAsync(vehicle.GunReference.ManufacturerReference.BudgetReference);
                    else
                    {
                        duplicate.GunReference.ManufacturerReference.BudgetReference = gunManufacturerBudget;
                        duplicate.GunReference.ManufacturerReference.BudgetId = gunManufacturerBudget.Id;
                    }
                }


                if (vehicle.EngineReference.ManufacturerReference.BudgetReference is not null)
                {
                    Budget engineManufacturerBudget = context.Budgets.Where(b => b.Value.Equals(vehicle.EngineReference.ManufacturerReference.BudgetReference.Value) &&
                    b.Currency.Equals(vehicle.EngineReference.ManufacturerReference.BudgetReference.Currency)).ToList().FirstOrDefault();

                    if (engineManufacturerBudget is null)
                        await context.Budgets.AddAsync(vehicle.EngineReference.ManufacturerReference.BudgetReference);
                    else
                    {
                        duplicate.EngineReference.ManufacturerReference.BudgetReference = engineManufacturerBudget;
                        duplicate.EngineReference.ManufacturerReference.BudgetId = engineManufacturerBudget.Id;
                    }
                }


                if (vehicle.ManufacturerReference.BudgetReference is not null)
                {
                    Budget tankManufacturerBudget = context.Budgets.Where(b => b.Value.Equals(vehicle.ManufacturerReference.BudgetReference.Value) &&
                    b.Currency.Equals(vehicle.ManufacturerReference.BudgetReference.Currency)).ToList().FirstOrDefault();

                    if (tankManufacturerBudget is null)
                        await context.Budgets.AddAsync(vehicle.ManufacturerReference.BudgetReference);
                    else
                    {
                        duplicate.ManufacturerReference.BudgetReference = tankManufacturerBudget;
                        duplicate.ManufacturerReference.BudgetId = tankManufacturerBudget.Id;
                    }
                }



                if (vehicle.GunReference.ManufacturerReference is not null)
                {
                    Manufacturer gunManufacturer = context.Manufacturers.Where(b => b.Name.Equals(vehicle.GunReference.ManufacturerReference.Name)).ToList().FirstOrDefault();
                    if (gunManufacturer is null)
                        await context.Manufacturers.AddAsync(vehicle.GunReference.ManufacturerReference);
                    else
                    {
                        duplicate.GunReference.ManufacturerReference = gunManufacturer;
                        duplicate.GunReference.ManufacturerId = gunManufacturer.Id;
                    }
                }


                if (vehicle.EngineReference.ManufacturerReference is not null)
                {
                    Manufacturer engineManufacturer = context.Manufacturers.Where(b => b.Name.Equals(vehicle.EngineReference.ManufacturerReference.Name)).ToList().FirstOrDefault();
                    if (engineManufacturer is null)
                        await context.Manufacturers.AddAsync(vehicle.EngineReference.ManufacturerReference);
                    else
                    {
                        duplicate.EngineReference.ManufacturerReference = engineManufacturer;
                        duplicate.EngineReference.ManufacturerId = engineManufacturer.Id;
                    }
                }


                if (vehicle.ManufacturerReference is not null)
                {
                    Manufacturer tankManufacturer = context.Manufacturers.Where(b => b.Name.Equals(vehicle.ManufacturerReference.Name)).ToList().FirstOrDefault();
                    if (tankManufacturer is null)
                        await context.Manufacturers.AddAsync(vehicle.ManufacturerReference);
                    else
                    {
                        duplicate.ManufacturerReference = tankManufacturer;
                        duplicate.ManufacturerId = tankManufacturer.Id;
                    }
                }


                if (vehicle.GunReference is not null)
                {
                    Gun gun = context.Guns.Where(g => g.ModelName.Equals(vehicle.GunReference.ModelName)).ToList().FirstOrDefault();
                    if (gun is null)
                        await context.Guns.AddAsync(vehicle.GunReference);
                    else
                    {
                        duplicate.GunReference = gun;
                        duplicate.GunId = gun.Id;
                    }
                }


                if (vehicle.EngineReference is not null)
                {
                    Engine engine = context.Engines.Where(e => e.ModelName.Equals(vehicle.EngineReference.ModelName)).ToList().FirstOrDefault();
                    if (engine is null)
                        await context.Engines.AddAsync(vehicle.EngineReference);
                    else
                    {
                        duplicate.EngineReference = engine;
                        duplicate.EngineId = engine.Id;
                    }
                }

                
                await context.Tanks.AddAsync(duplicate);

            }

            context.SaveChanges();
        }


        public IServiceProvider GetProvider()
        {
            return serviceCollection.BuildServiceProvider();
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public TankFactoryApplication()
        {
            serviceCollection = new ServiceCollection();
            
            serviceCollection.AddDbContext<VehicleDatabaseContext>();
        }

    }
}
