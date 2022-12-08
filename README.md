# Ski-Shop - Web Project 

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#built-with">Built With</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#try-it-in-azure">Try it in Azure</a></li>
  </ol>
</details>

## About The Project

<p align="center">
  <img src="https://user-images.githubusercontent.com/85368212/205940865-663b7252-6eb8-4689-8c50-a2c67da7159c.png" width="700" alt="accessibility text">
</p>

The website is a shop where you can buy skis and snowboards. The site was created for educational purposes. 

### User

The website provides registration and login functionality. You can create a new account and log-in with it or log-in with one of three different providers: 

- Facebook
- Google
- Microsoft

Once registered, you will be able to purchase any of the products and place an order to be processed by the administrators. 

### Administrator 

As an administrator you can:

- Add new Products (Skis or Snowboards)
  - You can **upload image** for each product. Images will be uploaded to a cloud service **[Cloudinary](https://cloudinary.com/console/c-054f2b0f7435ef7f8dff7f6ceec5bd/media_library/folders/home)**.
- Check **how many users are registered**.
  - Option to **promote** a user to **administrator and vice versa**. 
- Check what orders **have been placed by users**.
  - Option to **finish the order**.
- Add or remove **Roles**.
- Check all products **that have been added**.
  - Option to **remove one** from product quantity.
  - Option to **remove the product**.
  - Option to **edit the product**.
- Check **all removed products**.
  - Option to to **recover removed product** with a **quantity of your choice**.
- Check all **types, models and brands**.
  - **Type e.g. Ski or Snowboard**.
  - **Model e.g "HYPER" or "GWO"**.
  - **Brand e.g. "LIBTECH" or "GNU"**.
- **Add new type brand or model**.

**The project will be defended in front of technical trainers from [SoftUni](https://softuni.bg/) for final evaluation for the ASP.NET course.**

## Built With

![csharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![EFCore](https://img.shields.io/badge/Entity_Framework_Core-5C2D91?style=for-the-badge&logo=&logoColor=white)
![mssql](https://img.shields.io/badge/MSSQL-07405E?style=for-the-badge&logo=microsoft&logoColor=white)
![Bootstrap](https://img.shields.io/badge/bootstrap-%23563D7C.svg?style=for-the-badge&logo=bootstrap&logoColor=white)

## Getting Started

1. Clone the repo
   ```sh
   git clone https://github.com/PetarPaunov/Ski-Shop.git
   
2. Start and rebuild the project.
3. Configure **connection string** and **credentials** for **external providers** in the **appsettings JSON** file.
3. Configure the **credentials** for **[Cloudinary](https://cloudinary.com/console/c-054f2b0f7435ef7f8dff7f6ceec5bd/media_library/folders/home) service** in the **appsettings JSON** file.
4. Open the **"Package Manager Console"**, select the **.Data** project and **apply the migrations**.
   ```sh
   Update-Database
   
![image](https://user-images.githubusercontent.com/85368212/206008220-ce062f06-19fa-4797-9081-c420a67d2f5e.png)

## License

Distributed under the MIT License. See [**LICENSE.txt**](LICENSE) for more information.

## Try it 

You can try the application [here](http://petarpaunov-001-site1.btempurl.com/)

Login credentials:
Email: Test@Test.com
Password: qaz123



