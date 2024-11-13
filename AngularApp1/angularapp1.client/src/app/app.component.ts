import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

interface CdbCalculado {
  resultadoBruto: number;
  resultadoLiquido: number;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'angular18forms';
  public cdbCalculado: CdbCalculado = {} as CdbCalculado;
  public mensagemErro: string = '';

  constructor(private readonly http: HttpClient) { }

  onSubmit(form: NgForm) {
    this.mensagemErro = '';

    if (!form.value.valorInicial) {
      this.mensagemErro = "Valor inicial do investimento obrigatório"
      return;
    }

    if (!form.value.qtdeMeses) {
      this.mensagemErro = "Prazo em meses para resgate obrigatório"
      return;
    }

    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    this.http.post<CdbCalculado>('/cdb',
      { qtdeMeses: form.value.qtdeMeses, valorInicial: form.value.valorInicial },
      { headers: headers })
      .pipe(
        catchError(error => {
          console.log('status de erro: ', error.status);
          if (error.status == 400) {
            this.cdbCalculado = {} as CdbCalculado;
            this.mensagemErro = error.error;
          }
          return throwError(() => new Error('Oops! Something went wrong. Please try again later.'));
        })
      )      .subscribe(data => {
        this.cdbCalculado = data;
        console.log('Your post result data:', data);
      })
    console.log('Your form data:', form.value);
  }
}
