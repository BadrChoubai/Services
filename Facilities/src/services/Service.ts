export class Service<T> {
  protected dataStore: T[];

  constructor(dataStore: T[]) {
    this.dataStore = dataStore;
  }

  getAll(): T[] {
    return this.dataStore;
  }

  getById(id: string): T | null {
    return this.dataStore.find((i: any) => i.id === id);
  }
}
