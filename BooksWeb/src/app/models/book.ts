  
export interface Book {
    Id: Number;
    Name: String;
    Published: Boolean;
    Authors: String;
    PDFName: String;
}

export interface Books{
    books:Book[];
    count:number;
}
