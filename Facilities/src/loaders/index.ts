import express from "./express.js";
import type { Application } from "express";

/**
 * Configure and initialize any external services (infrastructure, side effects, etc.)
 * used by our Express application asynchronously
 *
 */
export default async ({ app: expressApp }: { app: Application }) => {
  await express(expressApp);
};
