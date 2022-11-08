import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { PacienteService } from '../paciente.service';

@Component({
  selector: 'app-listado',
  templateUrl: './listado.component.html',
  styleUrls: ['./listado.component.css']
})
export class ListadoComponent implements OnInit {

  displayedColumns: string[] = ['apellidos','nombres','direccion','hospital']


  constructor(private pacienteService: PacienteService) { }

  ngOnInit(): void {
    this.pacienteService.listarPacientes();
  }

  get resultados()
  {
    return this.pacienteService.resultados;
  }

  get totalRegistros()
  {
    return this.pacienteService.totalRegistros;
  }

  get registrosPorPagina()
  {
    return this.pacienteService.registrosPorPagina;
  }

  onPaginadoChange(event: PageEvent)
  {
    let pagina = event.pageIndex;
    let size = event.pageSize;
    pagina++;
    this.pacienteService.listarPacientes(pagina, size);

  }
}
