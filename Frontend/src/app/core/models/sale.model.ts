export interface Sale {
  saleNumber?: string;
  saleDate: string;
  customerId: string;
  customerName: string;
  branchName: string;
  totalAmount: number;
  items: SaleItem[];
  discount?: number;
  isCancelled: boolean;
}


export interface SaleItem {
  id: string;
  productId: string;
  unitPrice: number;
  quantity: number;
  discount: number;
  isCancelled: boolean;
}

export interface CreateSale {
  saleDto: Omit<Sale, 'saleNumber' | 'discount'>;
}

export interface UpdateSale {
  saleNumber: string;
  saleDate: string;
  customerId: string;
  customerName: string;
  branchId: string;
  branchName: string;
  items: SaleItem[];
}