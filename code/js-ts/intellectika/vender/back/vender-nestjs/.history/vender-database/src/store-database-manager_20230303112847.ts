import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"
import { IProduct } from "../../src/iproduct/iproduct.interface"

export class StoreDatabaseManager {

    constructor() {
        if (!AppDataSource.isInitialized) AppDataSource.initialize()
        this.productRepository = AppDataSource.getRepository(Product)
        this.ratingRepository = AppDataSource.getRepository(Rating)
    }

    readonly productRepository
    readonly ratingRepository


    public addProduct(product: Product) {
        this.performOperation(() => {
            this.productRepository.save(product)
            this.printReport('add-product')  
        })
    }


    public addRating(rating: Rating) {
        this.performOperation(() => {
            this.ratingRepository.save(rating)
            this.printReport('add-rating')  
        })
    }


    public getAllProducts(): IProduct[] {
        if (this.productRepository.count() === 0){

        }
        let products: IProduct[] = []
        this.performOperation(async () => {
            for (let i = 0, iSize = await this.productRepository.count(); i < iSize; i++) {
                let item = this.productRepository.findBy({
                    id: i
                })
                products.push(item)
                console.log(item.title)
            }
        })
        return products
    }



    public clearTables() {
        this.performOperation(async () => {
            let manager: EntityManager = new EntityManager(AppDataSource)
            await manager.query('USE StoreDatabase; DELETE FROM rating; DBCC CHECKIDENT (rating, RESEED, 0)')
            this.printReport('clear')  
        })
    }


    private printReport(methodName: string): void {
        console.log('[manual] ' + methodName + '-execution-finished')
    }


    private performOperation(callback) {
        callback()
    }


    private fillDatabase() {
        let rawProductsJson = fs.readFileSync('../../data/json/products.json')
        let arr = Iproducts
    }

}