import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"

@Entity()
export class Product {

    @PrimaryGeneratedColumn()
    id: number

    @Column()
    title: string

    @Column()
    price: number
    description: string
    category: string
    image: string

}
