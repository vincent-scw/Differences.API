import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { QuestionsRoutingModule } from './questions-routing.module';

import { QuestionListComponent } from './question-list.component';
import { AskQuestionComponent } from './ask-question.component';
import { QuestionDetailComponent } from './question-detail.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    QuestionsRoutingModule
  ],
  declarations: [
    QuestionDetailComponent,
    QuestionListComponent,
    AskQuestionComponent
  ],
  providers: []
})
export class QuestionsModule {}
