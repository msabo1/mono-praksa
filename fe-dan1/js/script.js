let todos = [];

document.getElementById('addButton').onclick = function () {
    const description = document.getElementById('addTodo').value;
    const todo = {
        description: description,
        status: 'open'
    }
    todos.push(todo);
    renderTodo(todo);
}

const renderTodo = (todo) => {
    const container = document.getElementById('content');
    const todoContainer = document.createElement('div');
    todoContainer.setAttribute('class', 'todo');
    const descriptionContainer = document.createElement('div');
    descriptionContainer.style.display = 'flex';
    descriptionContainer.style.alignContent = 'center';
    const actionsContainer = document.createElement('div');
    const descriptionSpan = document.createElement('span');
    descriptionSpan.innerHTML = todo.description;
    const editButton = document.createElement('span');
    editButton.setAttribute('class', 'material-icons edit');
    editButton.innerHTML = 'edit';
    const doneButton = document.createElement('span');
    doneButton.setAttribute('class', 'material-icons done');
    doneButton.innerHTML = 'done';
    const restoreButton = document.createElement('span');
    restoreButton.setAttribute('class', 'material-icons done');
    restoreButton.innerHTML = 'restore';
    const deleteButton = document.createElement('span');
    deleteButton.setAttribute('class', 'material-icons delete');
    deleteButton.innerHTML = 'delete';
    const editInput = document.createElement('input');
    editInput.setAttribute('type', 'text');
    const saveButton = document.createElement('span');
    saveButton.setAttribute('class', 'material-icons edit');
    saveButton.innerHTML = 'save';

    if(todo.status === 'done'){
      descriptionSpan.style.textDecoration = 'line-through';
        doneButton.style.display = 'none';
        restoreButton.style.display = 'inline-block';
    }else{
        restoreButton.style.display = 'none';
    }

    actionsContainer.append(editButton);
    actionsContainer.append(doneButton);
    actionsContainer.append(restoreButton);
    actionsContainer.append(deleteButton);


    descriptionContainer.append(descriptionSpan);
    descriptionContainer.append(editInput);
    descriptionContainer.append(saveButton);

    editInput.value = todo.description;
    editInput.style.display = 'none';
    saveButton.style.display = 'none';

    todoContainer.append(descriptionContainer);
    todoContainer.append(actionsContainer);

    container.append(todoContainer);

    todoContainer.style.width = '0%';
    todoContainer.animate([{width: '50%'}], {duration: 100}).finished.then(() => { todoContainer.style.width = '50%'; });

    deleteButton.onclick = function () {
        const index = todos.indexOf(todo);
        todos.splice(index, 1);
        todoContainer.animate([{height: '0%'}], {duration: 100}).finished.then(() => { todoContainer.remove()});
    }

    doneButton.onclick = function () {
        todo.status = 'done';
        descriptionSpan.style.textDecoration = 'line-through';
        doneButton.style.display = 'none';
        restoreButton.style.display = 'inline-block';
    }

    restoreButton.onclick = function () {
        todo.status = 'open';
        descriptionSpan.style.textDecoration = 'none';
        doneButton.style.display = 'inline-block';
        restoreButton.style.display = 'none';
    }

    editButton.onclick = function () {
        editButton.style.display = 'none';
        descriptionSpan.style.display = 'none';
        editInput.style.display = 'inline-block';
        saveButton.style.display = 'inline-block';
    }

    saveButton.onclick = function () {
        todo.description = editInput.value;
        descriptionSpan.innerHTML = editInput.value;
        editButton.style.display = 'inline-block';
        descriptionSpan.style.display = 'inline-block';
        editInput.style.display = 'none';
        saveButton.style.display = 'none';
    }


} 