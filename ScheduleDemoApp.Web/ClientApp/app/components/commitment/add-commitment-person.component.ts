import { Component, Input } from '@angular/core';
import { CommitmentPerson } from '../../models/commitment-person.model';
import { CommitmentService } from '../../services/commitment.service';
import { PersonService } from '../../services/person.service';
import { Person } from '../../models/person.model';

@Component({
    selector: 'add-commitmentperson',
    templateUrl: './add-commitment-person.component.html'
})

export class AddCommitmentPersonComponent {
    commitmentperson: CommitmentPerson = new CommitmentPerson();
    @Input() commitmentId: number;
    people: Array<Person> = new Array<Person>();

    constructor(private commitmentService: CommitmentService, private personService: PersonService) {
        this.personService.people.subscribe(people => {
            this.people = people;
        });

        this.personService.getPeople();
    }

    addCommitmentPerson(person: Person) {
        this.commitmentperson.commitmentId = this.commitmentId;
        this.commitmentperson.personId = person.id;

        this.commitmentService.addCommitmentPerson(this.commitmentperson);
    }
}