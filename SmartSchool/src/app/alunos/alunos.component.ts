import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Aluno } from '../models/Aluno';
import { AlunosService } from './alunos.service';

@Component({
  selector: 'app-alunos',
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.css']
})
export class AlunosComponent implements OnInit {
  public modalRef?: BsModalRef;
  public alunoForm!: FormGroup;
  public titulo = 'Alunos';
  public alunoSelected: Aluno | undefined;
  public textSimple: string | undefined;
  
  public alunos?: Aluno[];
 
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  
  constructor(private fb: FormBuilder, private modalService: BsModalService, private alunosService: AlunosService) {
    this.criarForm();
  }

  ngOnInit(): void {
    this.carregarAlunos();
  }

  carregarAlunos() {
    this.alunosService.getAll().subscribe(
      (alunos: Aluno[]) => {
        this.alunos = alunos;
      },
      (erro: any) => {
        console.error(erro);
      }
    );
  }

  criarForm() {
    this.alunoForm = this.fb.group({
      id: [''],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      telefone: ['', Validators.required]
    });
  }

  salvarAluno(aluno: Aluno) {
    if (aluno.id === 0) {
      this.alunosService.post(aluno).subscribe(
        () => {
          console.log(aluno);
          this.carregarAlunos();
        },
        (erro: any) => {
          console.error(erro);
        }
      );
    } else {
      this.alunosService.put(aluno).subscribe(
        () => {
          console.log(aluno);
          this.carregarAlunos();
        },
        (erro: any) => {
          console.error(erro);
        }
      );
    }
  }

  alunoSubmit() {
    this.salvarAluno(this.alunoForm.value);
  }

  alunoSelect(aluno: Aluno){
    this.alunoSelected = aluno;
    this.alunoForm.patchValue(aluno);
  }

  alunoNovo() {
    this.alunoSelected = new Aluno();
    this.alunoForm.patchValue(this.alunoSelected);
  }

  voltar(){
    this.alunoSelected = undefined;
  }
}
