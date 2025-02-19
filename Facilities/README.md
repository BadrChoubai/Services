# Facilities

This is a simple TypeScript-based API demo using Express.js. It provides a development and production setup with
TypeScript compilation and linting.

## Prerequisites

Ensure you have the following installed:

- [Node.js](https://nodejs.org/) (Latest LTS recommended)
- [npm](https://www.npmjs.com/) (Comes with Node.js)

#### Development Mode (with Hot Reloading)

```sh
npm run dev
```

This will start the TypeScript compiler in watch mode and run the server using `nodemon`.

#### Production Mode

First, build the project:

```sh
npm run build
```

Then, start the server:

```sh
npm run serve
```

### Linting

To run ESLint and automatically fix issues:

```sh
npm run lint
```
