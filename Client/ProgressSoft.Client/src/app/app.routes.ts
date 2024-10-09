import { Routes } from '@angular/router';
import { AllCardreadersComponent } from './all-cardreaders/all-cardreaders.component';
import { CreateCardrReaderComponent } from './create-cardr-reader/create-cardr-reader.component';
import { ManualCreateComponent } from './manual-create/manual-create.component';
import { ImportCreateComponent } from './import-create/import-create.component';

export const routes: Routes = [

    {
        path: "",
        component: AllCardreadersComponent
    },
    {
        path: "create",
        component: CreateCardrReaderComponent
    },
    {
        path: 'manual-create',
        component: ManualCreateComponent
    },
    {
        path: 'import-create',
        component: ImportCreateComponent
    }

];
