import { Controller, Get } from '@nestjs/common';
import { PrismaClient } from '@prisma/client';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  getHello(): string {
    const prisma = new PrismaClient()
    prisma.$connect
    prisma.rating.
    prisma.product.create
    return this.appService.getHello();
  }
}
