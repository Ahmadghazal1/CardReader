export interface ICardReader {
    id: number;
    name: string;
    address: string;
    phone: string;
    dateOfBirth: string;
    email: string;
    photo: string;
    gender: string;
}


export interface ImportCardReader {
    Name: string;
    Address: string;
    Phone: string;
    DateOfBirth: string;
    Email: string;
    Photo: string;
    Gender: string;
}

export interface ExportCardReader {
    Name: string;
    Address: string;
    Phone: string;
    DateOfBirth: string;
    Email: string;
    Photo: string;
    Gender: string;
}

export interface ReviewCardReader {
    name: string;
    address: string;
    phone: string;
    dateOfBirth: string;
    email: string;
    photo: string;
    gender: string;
}