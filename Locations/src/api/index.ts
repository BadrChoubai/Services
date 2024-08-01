import { Router } from 'express';
import locations from './routes/locations.js';

export default () => {
    const app = Router();

    locations(app);

    return app;
};