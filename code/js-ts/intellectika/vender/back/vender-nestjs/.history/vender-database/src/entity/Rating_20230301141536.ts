import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"

@Entity()
export class Rating {

    @PrimaryGeneratedColumn()
    id: number

    
}