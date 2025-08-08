export interface Customer {
  id: string;
  customerName: string;
}

export interface CreateCustomer {
  customerName: string;
}

export interface UpdateCustomer {
  id: string;
  customerName: string;
}