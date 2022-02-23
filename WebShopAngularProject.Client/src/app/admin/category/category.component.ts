import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/_models/category';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  categories: Category[] = [];
  category: Category = { id: 0, name: '', description: '' };

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getAllCategories()
      .subscribe({
        next: (x) => {
          this.categories = x;
        },
        error: (err) => {
          console.log(err);
        }
      })
  }

  save(): void {
    if (this.category.id == 0) {
      if (confirm('Save new category?')) {
        this.categoryService.createCategory(this.category)
          .subscribe({
            next: (x) => {
              this.categories.push(x);
              this.category = this.resetCategory();
            },
            error: (err) => {
              console.log(err);
            }
          });
      };
    } else {
      if (confirm('Update category with ID ' + this.category.id + '?')) {
        this.categoryService.updateCategory(this.category.id, this.category)
          .subscribe({
            next: () => {
              this.category = this.resetCategory();
            },
            error: (err) => {
              console.log(err);
            }
          })
      }
    }
  }

  edit(category: Category): void {
    if (confirm('Do you want to edit this category?')) {
      this.category = category;
    }
  }

  delete(category: Category): void {
    if (confirm('Do you want to delete this category?')) {
      this.categoryService.deleteCategory(category.id)
        .subscribe({
          next: (x) => {
            this.categories = this.categories.filter(x => x.id != category.id);
          },
          error: (err) => {
            console.log(err);
          }
        });
    };
  }

  cancel(): void {
    this.category = this.resetCategory();
  }

  resetCategory(): Category {
    var category: Category = { id: 0, name: '', description: '' };
    return category;
  }
}
