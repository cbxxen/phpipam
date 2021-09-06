export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T>{
    result: T; 
    //pagination Information is stored in pagination Interface (top) 
    pagination: Pagination;
}