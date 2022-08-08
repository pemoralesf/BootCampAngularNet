import { Component, OnInit } from '@angular/core';
import { HospitalService } from '../hospital.service';

@Component({
  selector: 'app-listado',
  templateUrl: './listado.component.html',
  styleUrls: ['./listado.component.css']
})
export class ListadoComponent implements OnInit {

  displayedColumns:  string[] = ['id','nombreHospital','direccion','telefono','correo'];

  constructor(private hospitalService: HospitalService) { }

  ngOnInit(): void {
    this.hospitalService.listarHospital();
  }

  get resultados(){

    return this.hospitalService.resultados;
  }
}
