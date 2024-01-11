import { Component, OnInit } from '@angular/core';
import { AnimalService } from '../animal.service';
import { Animal } from '../animal.model';

@Component({
  selector: 'app-animal',
  templateUrl: './animal.component.html',
  styleUrls: ['./animal.component.css'],
})
export class AnimalComponent implements OnInit {
  animals: Animal[] = [];
  newAnimalName: string = '';
  errorMessage: string = '';

  constructor(private animalService: AnimalService) {}

  ngOnInit(): void {
    this.loadAnimals();
  }

  loadAnimals(): void {
    this.animalService.getAnimals().subscribe({
      next: (data) => {
        this.animals = data;
      },
      error: (error) => {
        console.error('Error fetching animals', error);
        this.errorMessage = 'Error fetching animals';

        setTimeout(() => {
          this.errorMessage = '';
        }, 2000);
      },
    });
  }

  addAnimal(): void {
    const newAnimal = new Animal(this.newAnimalName);
    this.animalService.addAnimal(newAnimal).subscribe({
      next: () => {
        this.animals.push(newAnimal);
        this.newAnimalName = ''; // Reset the input field
      },
      error: (error) => {
        console.error('Error adding animal', error);
        this.errorMessage = 'Error adding animal';

        setTimeout(() => {
          this.errorMessage = '';
        }, 2000);
      },
    });
  }

  removeAnimal(animalName: string): void {
    this.animalService.removeAnimal(animalName).subscribe({
      next: () => {
        this.animals = this.animals.filter(
          (animal) => animal.name !== animalName
        );
      },
      error: (error) => {
        console.error('Error removing animal', error);
        this.errorMessage = 'Error removing animal';

        setTimeout(() => {
          this.errorMessage = '';
        }, 2000);
      },
    });
  }
}
