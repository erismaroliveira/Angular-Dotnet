import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Professor } from '../models/Professor';
import { ProfessoresService } from './professores.service';

@Component({
  selector: 'app-professores',
  templateUrl: './professores.component.html',
  styleUrls: ['./professores.component.css']
})
export class ProfessoresComponent implements OnInit {
  public modalRef?: BsModalRef;
  public professorForm!: FormGroup;
  public titulo = 'Professores';
  public profSelected: Professor | undefined;

  public professores?: Professor[];

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { id: 1, class: 'modal-lg' });
  }

  constructor(private fb: FormBuilder, private modalService: BsModalService, private professoresService: ProfessoresService) {
    this.criarForm();
   }

  ngOnInit() {
    this.carregarProfessores();
  }

  carregarProfessores() {
    this.professoresService.getAll().subscribe(
      (professores: Professor[]) => {
        this.professores = professores;
      },
      (erro: any) => {
        console.error(erro);
      }
    );
  }

  criarForm() {
    this.professorForm = this.fb.group({
      id: [''],
      nome: ['', Validators.required]
    });
  }

  salvarProfessor(professor: Professor) {
    if (professor.id === 0) {
      this.professoresService.post(professor).subscribe(
        () => {
          console.log(professor);
          this.carregarProfessores();
        },
        (erro: any) => {
          console.error(erro);
        }
      );
    } else {
      this.professoresService.put(professor).subscribe(
        () => {
          console.log(professor);
          this.carregarProfessores();
        },
        (erro: any) => {
          console.error(erro);
        }
      );
    }
  }

  professorSubmit() {
    this.salvarProfessor(this.professorForm.value);
  }

  profSelect(professor: Professor){
    this.profSelected = professor;
    this.professorForm.patchValue(professor);
  }

  professorNovo() {
    this.profSelected = new Professor();
    this.professorForm.patchValue(this.profSelected);
  }

  voltar(){
    this.profSelected = undefined;
  }
}
