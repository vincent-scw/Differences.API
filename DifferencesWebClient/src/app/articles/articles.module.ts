import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill';

import { ArticlesRoutingModule } from './articles-routing.module';

import { ArticleListComponent } from './article-list.component';
import { ArticleDetailComponent } from './article-detail.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    QuillModule,
    ArticlesRoutingModule
  ],
  declarations: [
    ArticleListComponent,
    ArticleDetailComponent
  ],
  providers: []
})
export class ArticlesModule {}
