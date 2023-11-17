import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-desenvolvedor',
  templateUrl: './desenvolvedor.component.html'
})
export class DesenvolvedorComponent {
  public repositories: string[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<string[]>(baseUrl + 'ProjetosParaRevisao').subscribe(result => {
      this.repositories = result;
    }, error => console.error(error));
  }
}
