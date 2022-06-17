const PAGINATION_DISPLAY_OFFSET = 2;
const DEFAULT_ELEMENTS_PER_PAGE = 5;

export default class PaginatedTable extends HTMLElement {

	constructor() {
		super();
		this.dataStorage = {
			data: null,
			currentPageData: null
		};
		this.pageParams = {
			elementsPerPage: null,
			currentPage: null,
			totalPages: null,
			totalElements: null
		};

		const styles = `
table {
    width: 100%;
}

    table tr {
        padding: 0;

    }

    table td, table th {
        padding: 0.25em;
    }

    table th {
       background-color: lightblue;
       color: black;
       font-weight: bold;
       text-transform: uppercase;

    }
    table td {
        border-bottom: 1px solid black;
        margin: 0;
        background-color: lightgray;
    }


button {
	  background: transparent;
	  border: none;
	  border-radius: 50%;
	  background: lightblue;
	  margin-right: 0.5rem;
	  margin-left: 0.5rem;
	  width: 2rem;
	  height: 2rem;
	}

	button:last-of-type {
	  margin-right: 0;
	}

	button:first-of-type {
	  margin-left: 0;
	}

	span {
	  font-size: 2rem;
	  text-align: left;
	}

	.pagination-container {
	  margin: 0.5rem 0;
	  display: flex;
	  justify-content: space-between;
	}
	table th, td{
	  text-align: left;
	}
	.highlight-current-page {
	  background: #003a78 !important;
	  color: lightgray !important;
	}`;
		this.shadow = this.attachShadow({ mode: 'open' });
		this.shadow.innerHTML = `<style>${styles}</style>
      <slot name="heading"></slot>
      <table> </table>
      <div class="pagination-container"></div>`;

		this.pageParams.elementsPerPage = parseInt(this.getAttribute('elementsPerPage')) || DEFAULT_ELEMENTS_PER_PAGE;

	

		this.loadData();

	}




	loadData() {
		if (this.hasAttribute('tableData')) {

			const paramKeys = this.getAttribute('dataKeys').split(',');
			let tableData = JSON.parse(this.getAttribute('tableData'))
			this.pageParams.totalElements = tableData.length;
			this.pageParams.totalPages = Math.ceil(this.pageParams.totalElements / this.pageParams.elementsPerPage);
			const thead = document.createElement('thead');
			this.dataStorage.data = tableData.map((item) => {
				let filteredMap = {};
				/* This is side effect to filter out the data */
				paramKeys.forEach((key) => {
					filteredMap[key] = item[key];
				});
				return filteredMap;
			});

			const tr = document.createElement('tr');
			const labels = this.getAttribute('columnLabels').split(',');

			paramKeys.forEach((key, index) => {
				const th = document.createElement('th');
				th.textContent = labels[index];
				tr.appendChild(th);
			});
			thead.appendChild(tr);

			this.dataStorage.currentPageData = this.dataStorage.data.slice(0, this.pageParams.elementsPerPage);
			const table = this.shadow.querySelector('table');
			const tbody = document.createElement('tbody');
			this.dataStorage.currentPageData.forEach((item) => {
				const tr = document.createElement('tr');
				Object.keys(item).forEach((key) => {
					const td = document.createElement('td');
					td.textContent = item[key];
					tr.appendChild(td)
				});
				tbody.appendChild(tr);
			});
			table.appendChild(thead);
			table.appendChild(tbody);

			const paginationContainer = this.shadow.querySelector('div.pagination-container');
			const previousButton = document.createElement('button');
			previousButton.textContent = '<';
			previousButton.addEventListener('click', this.paginationPrevious);
			paginationContainer.appendChild(previousButton);
			for (let i = 0; i < this.pageParams.totalPages; i++) {
				if (i < PAGINATION_DISPLAY_OFFSET || i >= this.pageParams.totalPages - PAGINATION_DISPLAY_OFFSET) {
					const button = document.createElement('button');
					button.textContent = i + 1;
					button.dataset.pageNum = i;
					button.addEventListener('click', this.handlePagination);
					if (i === 0) {
						button.classList.add('highlight-current-page');
					}
					paginationContainer.appendChild(button);
				}
				if (i >= PAGINATION_DISPLAY_OFFSET && i < this.pageParams.totalPages - PAGINATION_DISPLAY_OFFSET) {
					const span = document.createElement('span');
					span.textContent = '.';
					paginationContainer.appendChild(span);
				}
			}
			const nextButton = document.createElement('button');
			nextButton.textContent = '>';
			nextButton.addEventListener('click', this.paginationNext);
			paginationContainer.appendChild(nextButton);

		}

	}


	paginationNext = () => {
		const button = this.shadow.querySelector(`button[data-page-num="${this.pageParams.currentPage + 1}"]`);
		if (button && button.dataset.pageNum < this.pageParams.totalPages) {
			this.handlePagination({ target: button });
		}
	}

	paginationPrevious = () => {
		const button = this.shadow.querySelector(`button[data-page-num="${this.pageParams.currentPage - 1}"]`);
		if (button && button.dataset.pageNum >= 0) {
			this.handlePagination({ target: button });
		}
	}

	handlePagination = (event) => {
		const pageNum = parseInt(event.target.dataset.pageNum);
		this.pageParams.currentPage = pageNum;
		const tbody = this.shadow.querySelector('tbody');
		tbody.querySelectorAll('tr').forEach((ele) => ele.remove());
		this.dataStorage.currentPageData = this.dataStorage.data.slice(pageNum * this.pageParams.elementsPerPage, pageNum * this.pageParams.elementsPerPage + this.pageParams.elementsPerPage);
		this.dataStorage.currentPageData.forEach((item) => {
			const tr = document.createElement('tr');
			Object.keys(item).forEach((key) => {
				const td = document.createElement('td');
				td.textContent = item[key];
				tr.appendChild(td)
			});
			tbody.appendChild(tr);
		});
		const paginationContainer = this.shadow.querySelector('div.pagination-container');
		if (pageNum >= PAGINATION_DISPLAY_OFFSET - 1 && pageNum + 1 < this.pageParams.totalPages - PAGINATION_DISPLAY_OFFSET) {
			if (!this.shadow.querySelector('div.pagination-container').querySelector(`button[data-page-num="${pageNum + 1}"]`)) {
				const parentButton = this.shadow.querySelector('div.pagination-container').querySelector(`button[data-page-num="${pageNum}"]`);
				const button = document.createElement('button');
				button.addEventListener('click', this.handlePagination);
				button.dataset.pageNum = pageNum + 1;
				button.textContent = pageNum + 2;
				if (parentButton.nextSibling.nodeName === 'SPAN') {
					parentButton.nextSibling.remove();
				}
				this.shadow.querySelector('div.pagination-container').insertBefore(button, parentButton.nextSibling);
			}
		}
		this.shadow.querySelectorAll('button').forEach((button, index) => {
			const btnPageNum = parseInt(button.dataset.pageNum);
			if (btnPageNum !== 0 &&
				btnPageNum !== 1 &&
				btnPageNum < this.pageParams.totalPages - PAGINATION_DISPLAY_OFFSET &&
				btnPageNum !== pageNum &&
				btnPageNum !== pageNum + 1 &&
				btnPageNum !== pageNum - 1) {
				const span = document.createElement('span');
				span.textContent = '.';
				this.shadow.querySelector('div.pagination-container').insertBefore(span, button);
				button.remove();
			}
		})
		if (event.target.previousSibling && event.target.previousSibling.nodeName === 'SPAN') {
			const button = document.createElement('button');
			button.addEventListener('click', this.handlePagination);
			button.dataset.pageNum = pageNum - 1;
			button.textContent = pageNum;
			event.target.previousSibling.remove();
			this.shadow.querySelector('div.pagination-container').insertBefore(button, event.target);
		}
		this.shadow.querySelectorAll('button').forEach((button) => {
			if (pageNum !== parseInt(button.dataset.pageNum)) {
				if (button.classList.contains('highlight-current-page')) {
					button.classList.remove('highlight-current-page');
				}
			}
			else {
				button.classList.add('highlight-current-page');
			}
		});
	}
}


