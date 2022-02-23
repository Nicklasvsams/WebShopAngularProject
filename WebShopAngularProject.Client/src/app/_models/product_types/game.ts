import { Category } from "../category";
import { Product } from "../product";

export interface Game {
    id: number;
    publisher: string;
    publishedYear: number;
    language: string;
    genre: string;
    productId: number;
    categoryId: number;
    product?: Product;
    category?: Category;
}