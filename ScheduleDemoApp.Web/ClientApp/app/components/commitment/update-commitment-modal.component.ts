import { Component,Input } from '@angular/core';
import { Commitment } from '../../models/commitment.model';
import { CommitmentService } from '../../services/commitment.service';

@Component({
    selector: 'update-commitment-modal',
    templateUrl: './update-commitment-modal.component.html'
})

export class UpdateCommitmentModalComponent {
    @Input() modalID: string;
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