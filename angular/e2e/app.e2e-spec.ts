import { SchoolManagementTemplatePage } from './app.po';

describe('SchoolManagement App', function() {
  let page: SchoolManagementTemplatePage;

  beforeEach(() => {
    page = new SchoolManagementTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
