import { Component } from '@angular/core';
import { Commitment } from '../../models/commitment.model';
import { CommitmentService } from '../../services/commitment.service';

@Component({
    selector: 'update-commitment',
    templateUrl: './update-commitment.component.html'
})

export class UpdateCommitmentComponent {
    commitment: Commitment = new Commitment();

    constructor(private commitmentService: CommitmentService) {
        this.commitmentService.commitment.subscribe(commitment => {
            this.commitment = commitment;
        });
    }

    editCommitment() {
        this.commitmentService.updateCommitment(this.commitment);
    }
}