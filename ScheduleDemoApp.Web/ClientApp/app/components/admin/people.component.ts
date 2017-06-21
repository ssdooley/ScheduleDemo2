import { Component, OnInit } from '@angular/core';
import { PersonService } from '../../services/person.service';

@Component({
    selector: 'people',
    templateUrl: './people.component.html',
    providers: [ PersonService ]
})
export class PeopleComponent implements OnInit {
    constructor(private personService: PersonService) { }

    ngOnInit() {
        this.personService.getPeople();
    }
}