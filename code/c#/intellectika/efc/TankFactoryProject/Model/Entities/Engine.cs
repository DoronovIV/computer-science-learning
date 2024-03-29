﻿using Newtonsoft.Json.Linq;

namespace MainEntityProject.Model.Entities
{
    [Index(nameof(ModelName), IsUnique = true)]
    public class Engine
    {

        public string? ModelName { get; set; }

        public int? ManufacturerId { get; set; }

        public int? PriceId { get; set; }

        public Manufacturer? ManufacturerReference { get; set; }

        public Price? PriceReference { get; set; }

        public int Id { get; set; }

        public int? HorsePowers { get; set; }




        /// <summary>
        /// Equals method override.
        /// <br />
        /// Переопределение метода "Equals".
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Engine)
            {
                var engRef = obj as Engine;
                return engRef.ModelName.Equals(this.ModelName);
            }

            else return base.Equals(obj);
        }




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Engine()
        {
            ModelName = null;
            HorsePowers = 0;
            PriceReference = new();
            ManufacturerReference = new();
        }


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public Engine(string modelName, int horsePowers, Manufacturer manufacturerReference, Price? priceReference)
        {
            ModelName = modelName;
            HorsePowers = horsePowers;
            ManufacturerReference = manufacturerReference;
            PriceReference = priceReference;
        }

    }
}
