export interface Product {
  id: string;
  productName: string;
  unitPrice: number;
}

export interface CreateProduct {
  productName: string;
  unitPrice: number;
}

export interface UpdateProduct {
  id: string;
  productName: string;
  unitPrice: number;
}