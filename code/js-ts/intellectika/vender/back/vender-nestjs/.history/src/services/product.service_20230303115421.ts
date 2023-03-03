import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';
import * as fs from 'fs';
import { DatabaseService } from './database/database.service';
import { StoreDatabaseManager } from '../../vender-database/src/store-database-manager';

@Injectable()
export class ProductService {

    private products: IProduct[] = []

    public getProducts(): IProduct[] {
        return this.readProducts()
    }


    private readProducts() : IProduct[] {
        if (this.products.length === 0) {
            let databaseService: DatabaseService = new DatabaseService()
            await this.products = databaseService.getAll()
            console.log(this.products.length)
            return this.products
        }
        else return this.products
    }

}
