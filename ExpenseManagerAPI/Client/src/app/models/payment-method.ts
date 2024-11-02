export class PaymentMethod {
    name: string;
    billingDate: Date;
    status: Boolean;
    constructor() {
        this.name = '';
        this.billingDate = new Date();
        this.status = true;
    }
}
