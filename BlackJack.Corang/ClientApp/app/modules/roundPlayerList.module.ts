import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { RoundPlayerList } from '../components/roundPlayerList/roundPlayerList.component';

@NgModule({
    declarations: [
        RoundPlayerList
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forChild([
            {
                path: '',
                component: RoundPlayerList
            }
        ])
    ]
})
export class RoundPlayerListModule {
}
