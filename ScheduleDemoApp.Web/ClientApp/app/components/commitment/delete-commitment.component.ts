import { Component } from '@angular/core';
import { Commitment } from '../../models/commitment.model';
import { CommitmentService } from '../../services/commitment.service';

@Component({
    selector: 'delete-commitment',
    templateUrl: './delete-commitment.component.html'
})
export class DeleteCommitmentComponent {
    commitments: Array<Commitment> = new Array<Commitment>();

    constructor(private commitmentService: CommitmentService) {
        commitmentService.commitments.subscribe(commitments => {
            this.commitments = commitments;
        });
    }

    deleteCommitment(commitment: Commitment) {
        if (commitment.id) {
            this.commitmentService.deleteCommitment(commitment);
        }
    }
}