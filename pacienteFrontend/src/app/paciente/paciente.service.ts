import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IDataPaciente, Paciente } from './interfaces/IDataPaciente';

@Injectable({
  providedIn: 'root'
})
export class PacienteService {

  baseUrl: string = environment.apiUrl;
  empleadoUrl: string = `${this.baseUrl}/paciente`;
  resultados: Paciente[]  = [];

  constructor(private http: HttpClient) { }

  totalRegistros: number = 0;
  registrosPorPagina:  number = 0;

  listarPacientes(pagina: number=0, size: number = 4){
   const  params = new HttpParams()
                   .set('PageNumber',pagina.toString())
                   .set('PageSize', size.toString())

    this.http.get<IDataPaciente>(this.empleadoUrl, {params: params})
              .subscribe(resp => {
                this.resultados = resp.resultado;
                this.totalRegistros = resp.totalRegistros;
                this.registrosPorPagina = resp.pageSize;
              }   )
}

}
