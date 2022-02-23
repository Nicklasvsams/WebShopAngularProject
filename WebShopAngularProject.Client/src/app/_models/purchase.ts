import { User } from "./user";
import { Product } from "./product"

export interface Purchase {
    id: number;
    purchaseDate: Date;
    userId: number;
    productId: number;
    user?: User;
    product?: Product;
}