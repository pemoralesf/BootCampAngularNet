import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Hospital, IDataHospital } from './interfaces/IDataHospital';

@Injectable({
  providedIn: 'root'
})
export class HospitalService {

  apiUrl: string = environment.apiUrl;

  hospitalUrl: string = `${this.apiUrl}/hospital`;

  resultados: Hospital[] = [];

  constructor(private http: HttpClient) { }

  listarHospital(){

    this.http.get<IDataHospital>(this.hospitalUrl)
                              .subscribe(resp =>{
                              this.resultados = resp.resultado;

                              })
  }
}
