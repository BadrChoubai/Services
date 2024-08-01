import {Express} from "express";
import expressLoader from './express.js';

export default async (expressApp: Express ) => {

    await expressLoader({ app: expressApp });
    console.log('express loaded');
};