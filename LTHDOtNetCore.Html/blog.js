const BLOG_LOCAL_STORAGE = "blogs";
let blogId = null;

function testConfirmMessage() {
  let confirmMessage = new Promise(function (success, error) {
    const result = confirm("Do u really want to delete this?");
    if (result) {
      success();
    } else {
      error();
    }
  });

  confirmMessage.then(
    function (value) {
      successMessage("Success Twr p");
    },
    function (error) {
      errorMessage("Error LOL");
    }
  );
}

function testConfirmMessage2() {
  let confirmMessage = new Promise(function (success, error) {
    Swal.fire({
      title: "Confirm",
      text: "Delete Mhr Lr?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes",
    }).then((result) => {
      if (result.isConfirmed) {
        success();
      } else {
        error();
      }
    });
  });

  confirmMessage.then(
    function () {
      successMessage("Success");
    },
    function () {
      errorMessage("Error");
    }
  );
}

const getExistingBlogs = () => {
  const blogsJsonString = localStorage.getItem(BLOG_LOCAL_STORAGE);
  return blogsJsonString ? JSON.parse(blogsJsonString) : [];
};

getBlogTable();

const getBlog = (id) => {
  const blog = getBlogById(id);

  blogId = blog.id;
  $("#titleInput").val(blog.title);
  $("#authorInput").val(blog.author);
  $("#contentInput").val(blog.content);
  $("#titleInput").focus();
};

const createBlog = (title, author, content) => {
  let existingBlogs = getExistingBlogs();

  const requestBlog = {
    id: uuidv4(),
    title,
    author,
    content,
  };

  existingBlogs.push(requestBlog);

  const blogsJsonString = JSON.stringify(existingBlogs);
  localStorage.setItem(BLOG_LOCAL_STORAGE, blogsJsonString);

  alert("Successfully Saved");
  clearControls();
};

const updateBlog = (id, title, author, content) => {
  let blogs = getExistingBlogs();

  const blogIndex = blogs.findIndex((x) => x.id === id);

  if (blogIndex === -1) {
    alert("No blog found.");
    return;
  }

  blogs[blogIndex] = { ...blogs[blogIndex], title, author, content };

  localStorage.setItem(BLOG_LOCAL_STORAGE, JSON.stringify(blogs));
  blogId = null;
  alert("Successfully Updated");
};

function deleteBlog(id) {
  const result = confirm("Do you want to delete this?");
  if (!result) return;

  let blogs = getExistingBlogs();

  getBlogById(id);

  blogs = blogs.filter((x) => x.id !== id);
  localStorage.setItem(BLOG_LOCAL_STORAGE, JSON.stringify(blogs));

  alert("Successfully Deleted");

  getBlogTable();
}

const deleteBlog3 = (id) => {
  Swal.fire({
    title: "Confirm",
    text: "Are you sure want to delete?",
    icon: "warning",
    showCancelButton: true,
    confirmButtonText: "Yes",
  }).then((result) => {
    if (!result.isConfirmed) return;

    let blogs = getExistingBlogs();

    const blog = lst.find((b) => b.id === id);
    if (!blog) {
      return;
    }

    blogs = blogs.filter((x) => x.id !== id);
    localStorage.setItem(BLOG_LOCAL_STORAGE, JSON.stringify(blogs));

    successMessage("Successfully Deleted");

    getBlogTable();
  });
};

const getBlogById = (id) => {
  const blogs = getExistingBlogs();
  const blog = blogs.find((x) => x.id === id);
  if (!blog) {
    alert("No Blog Found");
    return;
  }
  return blog;
};

$("#blogSaveBtn").click(function () {
  const title = $("#titleInput").val();
  const author = $("#authorInput").val();
  const content = $("#contentInput").val();

  blogId
    ? updateBlog(blogId, title, author, content)
    : createBlog(title, author, content);

  getBlogTable();
});

function clearControls() {
  $("#titleInput").val("");
  $("#authorInput").val("");
  $("#contentInput").val("");
  $("#titleInput").focus();
}

function getBlogTable() {
  const blogs = getExistingBlogs();
  let htmlRows = "";
  blogs.forEach((blog, index) => {
    const htmlRow = `
        <tr>
            
            <td>${++index}</td>
            <td>${blog.title}</td>
            <td>${blog.author}</td>
            <td>${blog.content}</td>
            <td>
                <button type="button" class="btn btn-warning" onclick="getBlog('${
                  blog.id
                }')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${
                  blog.id
                }')">Delete</button>
            </td>
        </tr>
        `;
    htmlRows += htmlRow;
  });

  $("#tbody").html(htmlRows);
}
