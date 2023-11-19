import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-desenvolvedor',
  templateUrl: './desenvolvedor.component.html'
})
export class DesenvolvedorComponent {
  public projects: ProjetoCompilado[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ProjetoCompilado[]>(baseUrl + 'ProjetosCompilados').subscribe(result => {
      this.projects = result;
    }, error => console.error(error));
  }
}
interface ProjetoCompilado {
  project: string;
  compilation: string[];
}
