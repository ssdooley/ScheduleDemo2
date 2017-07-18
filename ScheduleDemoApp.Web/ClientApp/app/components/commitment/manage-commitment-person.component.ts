import { Component, Input } from '@angular/core';
import { Person } from '../../models/person.model';
import { CommitmentService } from '../../services/commitment.service';

@Component({
    selector: 'manage-commitment-person',
    templateUrl: './manage-commitment-person.component.html'
})

export class ManageCommitmentPersonComponent {
    @Input() people: Array<Person>;
    @Input() commitmentId: number;

    constructor(private commitmentService: CommitmentService) {}

    deleteCommitmentPerson(person: Person) {
        this.commitmentService.deleteCommitmentPerson(person, this.commitmentId);
    }
}
