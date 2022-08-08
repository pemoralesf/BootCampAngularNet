export interface IDataHospital {
  isExitoso: boolean;
  resultado: Hospital[];
  mensaje:   string;
}

export interface Hospital {
  id:             number;
  nombreHospital: string;
  direccion:      string;
  telefono:       string;
  correo:         string;
}
