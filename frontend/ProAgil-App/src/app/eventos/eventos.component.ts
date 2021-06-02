import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { Evento } from '../_models/Evento';
import { EventoService } from '../_services/evento.service';
import {defineLocale, BsLocaleService,  ptBrLocale} from 'ngx-bootstrap'
import { template } from '@angular/core/src/render3';
defineLocale('pt-br', ptBrLocale);
@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventosFiltrados: Evento[];
  evento: Evento;
  eventos: Evento[];

  modoSalvar = '';
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  bodyDeletarEvento = '';
  _filtroLista: string = '';
  get filtroLista(){
    return this._filtroLista;
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService
     ) 
     {
       this.localeService.use('pt-br');
     }

  openModal(template: any){
    this.registerForm.reset();
    template.show();
  }


  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) :  this.eventos;
  }


  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  getEventos() {
    this.eventoService.getAllEventos().subscribe(
      (_eventos: Evento[]) => { 
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos; 
        console.log(_eventos);
      },
      error => {
        console.log(error);
      }
    )
  }

  filtrarEventos(filtrarPor: string) : any {

    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(evento =>
      evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  showImagem(){
    this.mostrarImagem = !this.mostrarImagem; 
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: [
        '', 
      [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)
      ]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      imagemURL: ['', Validators.required],
      qtdPessoas: [
        '', 
      [
        Validators.required,
        Validators.max(120000)
      ]],
      telefone: ['', Validators.required],
      email: ['', 
      [
        Validators.required,
        Validators.email
      ]]
    });
    console.log(this.fb.group)
  }

  salvarAlteracao(template: any) {
    debugger
  if(this.registerForm.valid) {
    if(this.modoSalvar === 'post'){
      this.evento = Object.assign({}, this.registerForm.value);
      this.eventoService.postEvento(this.evento).subscribe(
        (novoEvento: Evento) => {
          console.log(novoEvento);
          template.hide();
          this.getEventos();
        },
        error => {
          console.log(error);
        }
      );
    }else{
      this.evento = Object.assign({id: this.evento.id}, this.registerForm.value);
    this.eventoService.putEvento(this.evento).subscribe(
      () => {
        template.hide();
        this.getEventos();
      },
      error => {
        console.log(error);
      }
    );
    }
  }
    
}

  novoEvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  editarEvento(template:any, evento: Evento){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema} de codigo ${evento.id}`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
      }, error => {
        console.log(error);
      }
    )
  }

}
