import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { RoundList } from '../components/roundList/roundList.component';

@NgModule({
    declarations: [
        RoundList
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forChild([
            {
                path: '',
                component: RoundList
            }
        ])
    ]
})
export class RoundListModule {
}
