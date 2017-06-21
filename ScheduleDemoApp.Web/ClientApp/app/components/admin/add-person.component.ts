import { Component } from '@angular/core';
import { Person } from '../../models/person.model';
import { PersonService } from '../../services/person.service';

@Component({
    selector: 'add-person',
    templateUrl: './add-person.component.html'
})
export class AddPersonComponent {
    person: Person = new Person();

    constructor(private personService: PersonService) {
        this.personService.newPerson.subscribe(person => {
            this.person = person;
        });
    }

    createPerson() {
        this.personService.addPerson(this.person);
    }
}