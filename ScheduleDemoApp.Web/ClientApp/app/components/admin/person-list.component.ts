import { Component } from '@angular/core';
import { Person } from '../../models/person.model';
import { PersonService } from '../../services/person.service';

@Component({
    selector: 'person-list',
    templateUrl: './person-list.component.html'
})
export class PersonListComponent {
    people: Array<Person> = new Array<Person>();

    constructor(private personService: PersonService) {
        personService.people.subscribe(people => {
            this.people = people;
        });
    }

    editPerson(person: Person) {
        if (person.name) {
            this.personService.updatePerson(person);
        }
    }

    deletePerson(person: Person) {
        if (person.id) {
            this.personService.deletePerson(person);
        }
    }
}