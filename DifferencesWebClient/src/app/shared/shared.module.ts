import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';
import { QuillModule } from 'ngx-quill';
import { ApolloModule } from 'apollo-angular';

import { provideClient } from '../services/apollo-client.service';

@NgModule({
    imports: [
        HttpModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        QuillModule,
        ApolloModule.forRoot(provideClient)
    ],
    exports: [
        HttpModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        QuillModule,
        ApolloModule,
    ]
})

export class SharedModule { }
