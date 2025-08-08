import { MongoMessage } from '../models/mongo.model';

export function mapMongoMessageTo<T>(message: MongoMessage | null): T | null {
  if (!message || !message.message) return null;

  try {
    return JSON.parse(message.message) as T;
  } catch {
    return null;
  }
}
