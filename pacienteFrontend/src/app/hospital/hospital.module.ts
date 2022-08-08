import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListadoComponent } from './listado/listado.component';
import { AgregarComponent } from './agregar/agregar.component';
import { EditarComponent } from './editar/editar.component';
import { EliminarComponent } from './eliminar/eliminar.component';
import { HomeComponent } from './home/home.component';
import { HospitalRoutingModule } from './hospital-routing.module';
import { MaterialModule } from '../material/material.module';
import { HospitalService } from './hospital.service';



@NgModule({
  declarations: [
    ListadoComponent,
    AgregarComponent,
    EditarComponent,
    EliminarComponent,
    HomeComponent

  ],
  imports: [
    CommonModule,
    HospitalRoutingModule,
    MaterialModule
  ],
  providers: [
    HospitalService
  ]
})
export class HospitalModule { }
