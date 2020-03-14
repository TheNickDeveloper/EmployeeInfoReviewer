
export class Person {
    id:number;
    firstName:string;
    lastName:string;
    age:number;
    addresses: Address[];
    emailAddresses: EmailAddress[];
}

export class EmailAddress {
    id:number;
    emailAddress:string;
}

export class Address {
    id:number;
    streetAddress: string;
    city: string;
    state: string;
    zipCode: string;
}
