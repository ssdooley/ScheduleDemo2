﻿import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Commitment } from '../models/commitment.model';
import { CommitmentPerson } from '../models/commitment-person.model';
import { Person } from '../models/person.model';
import { ToastrService } from './toastr.service';

@Injectable()
export class CommitmentService {
    constructor(private http: Http, private toastrService: ToastrService) { }

    private commitmentsSubject = new Subject<Array<Commitment>>();
    private commitmentSubject = new Subject<Commitment>();
    private newCommitmentSubject = new Subject<Commitment>();

    commitments = this.commitmentsSubject.asObservable();
    commitment = this.commitmentSubject.asObservable();
    newCommitment = this.newCommitmentSubject.asObservable();

    getCommitments(): void {
        this.http.get('/api/commitment/getCommitments')
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(commitments => {
                this.commitmentsSubject.next(commitments);
            },
            error => {
                this.toastrService.alertDanger(error, "Get Commitments Error");
            });
    }
    getCommitment(id: number) {
        this.http.get('/api/commitment/getCommitment/' + id)
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(commitment => {
                this.commitmentSubject.next(commitment);
            },
            error => {
                this.toastrService.alertDanger(error, "Get Commitment Error");
            });
    }

    getPersonalCommitments(id: number): void {
        this.http.get('/api/commitment/getPersonalCommitments/' + id)
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(commitments => {
                this.commitmentsSubject.next(commitments);
            },
            error => {
                this.toastrService.alertDanger(error, "Get Personal Commitments Error");
            });
    }

    addCommitment(model: Commitment) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(model);

        return this.http.post('/api/commitment/AddCommitment', body, options)
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(res => {
                this.getCommitments();
                this.newCommitmentSubject.next(new Commitment());
                this.toastrService.alertSuccess(model.category + " " + model.people + " successfully added", "Add Commitment")
            },
            error => {
                this.toastrService.alertDanger(error, "Add Commitment Error");
            });
    }

    updateCommitment(model: Commitment) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(model);

        return this.http.post('/api/commitment/updateCommitment', body, options)
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(res => {
                this.getCommitments();
                this.toastrService.alertSuccess("Commitment successfully updated", "Updated Commitment")
            },
            error => {
                this.toastrService.alertDanger(error, "Update Commitment Error");
            });
    }

    addCommitmentPerson(model: CommitmentPerson) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(model);

        return this.http.post('/api/commitment/AddCommitmentPerson', body, options)
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(res => {
                this.getCommitments();
                this.newCommitmentSubject.next(new Commitment());
                this.toastrService.alertSuccess("Commitment successfully added person", "Add Commitment Person")
            },
            error => {
                this.toastrService.alertDanger(error, "Add Commitment Person Error");
            });
    }

    deleteCommitmentPerson(model: Person, id: number) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(model.commitmentPersonId);

        this.http.post('/api/commitment/deleteCommitmentPerson', body, options)
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(res => {
                this.getCommitment(id);
                this.toastrService.alertSuccess(model.name + ' successfully removed', 'Delete Commitment Person');
            },
            error => {
                this.toastrService.alertDanger(error, 'Delete Commitment Person Error');
            });
    }

    deleteCommitment(model: Commitment) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(model.id);

        return this.http.post('/api/Commitment/DeleteCommitment', body, options)
            .map(this.extractData)
            .catch(this.handleError)
            .subscribe(res => {
                this.getCommitments();
                this.toastrService.alertSuccess("Commitment successfully deleted", "Delete Commitment");
            },
            error => {
                this.toastrService.alertDanger(error, "Delete Commitment Error");
            });
    }

    private extractData(res: Response) {
        return res.json() || {};
    }


    private handleError(error: Response | any) {
        let errMsg: string;

        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText} || ''} ${err}`;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }

        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}