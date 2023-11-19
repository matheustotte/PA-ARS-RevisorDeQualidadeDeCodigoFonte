import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-coordenador',
  templateUrl: './coordenador.component.html'
})
export class CoordenadorComponent {
  public repositories: ProjetoParaRevisao[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ProjetoParaRevisao[]>(baseUrl + 'ProjetosParaRevisao').subscribe(result => {
      this.repositories = result;
    }, error => console.error(error));
  }
}
interface Projeto {
  projectPath: string;
  projectSelected: boolean;
}
interface ProjetoParaRevisao {
  repositoryName: string;
  projects: Projeto[];
}
