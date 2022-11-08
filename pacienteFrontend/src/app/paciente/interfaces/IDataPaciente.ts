export interface IDataPaciente {
  totalRegistros: number;
  totalPaginas:   number;
  pageSize:       number;
  statusCode:     number;
  isExitoso:      boolean;
  resultado:      Paciente[];
  mensaje:        string;
}

export interface Paciente {
  apellidos: string;
  nombres:   string;
  direccion: string;
  hospital:  string;
}
