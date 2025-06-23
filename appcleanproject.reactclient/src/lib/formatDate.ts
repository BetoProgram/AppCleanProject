import { format } from "date-fns";

export function formatDate(date:Date):string{
    const dateFormat = format(date, 'yyyy-MM-dd');
    return dateFormat;
}